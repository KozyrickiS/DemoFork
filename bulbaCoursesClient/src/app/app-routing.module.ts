import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SampleComponent } from './sample/components/sample/sample.component';
import { LoginComponent } from './auth/components/login/login.component';


const routes: Routes = [
  { path: '', component: SampleComponent, pathMatch: 'full' },
  { path: 'login', component: LoginComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
