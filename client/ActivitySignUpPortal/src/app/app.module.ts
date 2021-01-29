import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ActivityListComponent } from './pages/activity-list/activity-list.component';
import { ActivityComponent } from './pages/activity/activity.component';
import { NewActivityComponent } from './pages/activity/new/new.component';
import { 
  NbThemeModule, 
  NbLayoutModule, 
  NbCardModule,
  NbListModule,
  NbInputModule,
  NbButtonModule
 } from '@nebular/theme';

@NgModule({
  declarations: [
    AppComponent,
    ActivityListComponent,
    ActivityComponent,
    NewActivityComponent
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
    NbButtonModule
  ],
  providers: [ CookieService ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
