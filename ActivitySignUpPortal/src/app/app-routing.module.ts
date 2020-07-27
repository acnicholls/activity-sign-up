import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ActivityComponent } from './activity/activity.component';
import { ActivityListComponent } from './activity-list/activity-list.component';
import { NewActivityComponent } from './activity/new/new.component';

const routes: Routes = [
 // {path:'/', redirectTo:'activity-list'},
  { path: 'activity', component: ActivityComponent},
  { path: 'activity-list', component: ActivityListComponent },
  { path: 'activity/new', component: NewActivityComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
