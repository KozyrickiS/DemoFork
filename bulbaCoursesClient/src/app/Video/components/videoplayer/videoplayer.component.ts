import { Component, OnInit } from '@angular/core';
import { VgAPI } from 'videogular2/compiled/core';

export interface IMedia {
  title: string;
  src: string;
  type: string;
}

@Component({
  selector: 'app-videoplayer',
  templateUrl: './videoplayer.component.html',
  styleUrls: ['./videoplayer.component.scss']
})
export class VideoplayerComponent implements OnInit {
  playlist: Array<IMedia> = [
    {
        title: 'Pale Blue Dot',
        src: 'http://static.videogular.com/assets/videos/videogular.mp4',
        type: 'video/mp4'
    },
    {
        title: 'Big Buck Bunny',
        src: 'http://localhost:50401/Temp1/Landscape-757.mp4',
        type: 'video/mp4'
    },
    {
        title: 'Elephants Dream',
        src: 'http://static.videogular.com/assets/videos/elephants-dream.mp4',
        type: 'video/mp4'
    }
];

  currentIndex = 0;
  currentItem: IMedia = this.playlist[ this.currentIndex ];
  api: VgAPI;

  constructor() { }

  onPlayerReady(api: VgAPI) {
    this.api = api;

    this.api.getDefaultMedia().subscriptions.loadedMetadata.subscribe(this.playVideo.bind(this));
    this.api.getDefaultMedia().subscriptions.ended.subscribe(this.nextVideo.bind(this));
  }

  nextVideo() {
    this.currentIndex++;

    if (this.currentIndex === this.playlist.length) {
        this.currentIndex = 0;
    }

    this.currentItem = this.playlist[ this.currentIndex ];
  }

  playVideo() {
    this.api.play();
  }

  onClickPlaylistItem(item: IMedia, index: number) {
    this.currentIndex = index;
    this.currentItem = item;
  }

  ngOnInit() {
  }

}
