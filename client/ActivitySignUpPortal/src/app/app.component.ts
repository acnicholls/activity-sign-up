import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.sass']
})
export class AppComponent implements OnInit {

  title = 'Activity SignUp Portal';

  constructor(
    private routeService: Router
    ) { }

  ngOnInit(): void {
    // automatically send the user to the activity-list component
    this.routeService.navigateByUrl('/activity-list');
  }

}
