import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ActivityListComponent } from './pages/activity-list/activity-list.component';
import { ActivityComponent } from './pages/activity/activity.component';
import { NewActivityComponent } from './pages/activity/new/new.component';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { 
  NbThemeModule, 
  NbLayoutModule, 
  NbCardModule,
  NbListModule,
  NbInputModule,
  NbButtonModule,
  NbIconModule,
  NbActionsModule,
 } from '@nebular/theme';
import { UploadComponent } from './pages/activity/new/upload.component';

@NgModule({
  declarations: [
    AppComponent,
    ActivityListComponent,
    ActivityComponent,
    NewActivityComponent,
    UploadComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NbThemeModule.forRoot({name: 'dark' }),
    NbLayoutModule,
    NbCardModule,
    NbListModule,
    NbInputModule,
    NbButtonModule,
    NbIconModule,
    NbActionsModule,
    NbEvaIconsModule
  ],
  providers: [],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
