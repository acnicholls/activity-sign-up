import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ActivityComponent } from './pages/activity/activity.component';
import { ActivityListComponent } from './pages/activity-list/activity-list.component';
import { NewActivityComponent } from './pages/activity/new/new.component';

const routes: Routes = [
  { path: 'activity/new', component: NewActivityComponent, pathMatch: 'full' },
  { path: 'activity/:activityId', component: ActivityComponent },
  { path: 'activity-list', component: ActivityListComponent },
  { path: '', redirectTo: '/activity-list', pathMatch: 'full' },
  { path: '**', redirectTo: '/activity-list' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],  
  exports: [RouterModule]
})
export class AppRoutingModule { }
