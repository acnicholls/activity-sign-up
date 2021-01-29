import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ActivityComponent } from './pages/activity/activity.component';
import { ActivityListComponent } from './pages/activity-list/activity-list.component';
import { NewActivityComponent } from './pages/activity/new/new.component';

const routes: Routes = [
  { path: 'activity/new', component: NewActivityComponent },
  { path: 'activity/:activityId', component: ActivityComponent },
  { path: 'activity-list', component: ActivityListComponent },
];

@NgModule({
  //imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  imports: [RouterModule.forRoot(routes)],  
  exports: [RouterModule]
})
export class AppRoutingModule { }
