import { Component, OnInit } from '@angular/core';
import { YoutubeService } from '../../services/youtube.service';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder } from '@angular/forms';
import * as moment from 'moment';
import { User, CustomUser } from 'src/app/auth/models/user';
import { AuthService } from 'src/app/auth/services/auth.service';
import { SearchStory } from '../../models/searchstory';
import { ResultVideo } from '../../models/resultvideo';
import { SearchRequest } from '../../models/searchrequest';
import { pipe } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-search-request',
  templateUrl: './search-request.component.html',
  styleUrls: ['./search-request.component.scss']
})
export class SearchRequestComponent implements OnInit {

  resultVideos: ResultVideo[] = [];
  searchForm: FormGroup;
  parameter: string;
  youtubeService: YoutubeService;
  isAuthenticated: boolean;
  user: CustomUser;

  public loading = false;

  constructor(private service: YoutubeService, route: ActivatedRoute, fb: FormBuilder, private authService: AuthService) {
    // route.params.subscribe(params => this.parameter = params['name']);
    this.searchForm = fb.group({
      title: [''],
      published: ['Any'],
      definition: ['Any'],
      dimension: ['Any'],
      duration: ['Any'],
      caption: ['Any'],
    });
    this.youtubeService = service;
  }

  onSubmit() {
    if (this.searchForm.valid) {
      console.log('Search start..');

      const dataForm = this.searchForm.value;

      const searchRequest = new SearchRequest();
      searchRequest.Title = dataForm.title;
      searchRequest.Definition = dataForm.definition;
      searchRequest.Dimension = dataForm.dimension;
      searchRequest.Duration = dataForm.duration;
      searchRequest.VideoCaption = dataForm.caption;

      switch (dataForm.published) {
        case 'Hour':
          searchRequest.PublishedBefore = moment.utc().toDate();
          searchRequest.PublishedAfter = moment.utc().subtract(1, 'hour').toDate();
          break;
        case 'Today':
          searchRequest.PublishedBefore = moment().toDate();
          searchRequest.PublishedAfter = moment().startOf('day').toDate();
          break;
        case 'Week':
          searchRequest.PublishedBefore = moment().toDate();
          searchRequest.PublishedAfter = moment().startOf('isoWeek').toDate();
          break;
        case 'Month':
          searchRequest.PublishedBefore = moment().toDate();
          searchRequest.PublishedAfter = moment().startOf('month').toDate();
          break;
        case 'Year':
          searchRequest.PublishedBefore = moment().toDate();
          searchRequest.PublishedAfter = moment().startOf('year').toDate();
          break;
        default:
          searchRequest.PublishedBefore = null;
          searchRequest.PublishedAfter = null;
          break;
      }
      this.loading = true;
      this.youtubeService.searchVideo(searchRequest, this.user).subscribe(data => {
      this.loading = false;
      this.resultVideos = data;
      this.youtubeService.resultSubject.next(this.resultVideos);
      console.log('Search completed!');
      },
      (error) => console.log('Search exception...'),
      () => this.youtubeService.getStory(this.user)
      );
    }
  }
  ngOnInit() {
    this.authService.isAuthenticated$.subscribe((flag) => this.isAuthenticated = flag);
    this.authService.user$.subscribe((user) => this.user = user as CustomUser);
  }
}
