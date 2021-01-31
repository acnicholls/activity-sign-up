# ActivitySignUpPortal

## Site Map
The site really has only 3 pages, with 1 page having two views.

- Activity List - The main page where you can see the list of upcoming activities, any activity with a date earlier than today will not appear on the list

- New Activity - Add a new activity to the list of activities.

- Activity - the page with two views, the site determines if a user has signed up by caching data in the browser's local storage for the site.  
    - If the user has signed up, the site will display the participant and comments list.
    - If the user has NOT signed up for the event they will see the event details and the sign up page.

## Caveats
 - the user could use multiple browsers and the site would not know that they have signed up for the event.
 - some CSS is not up to par with Mobile devices, limitations of the theme I chose.
 - the site could benefit from some additional validation
 - activity images are uploaded independently of the remainder of the form, making form validation a little more difficult
 - some of the components could have been split out to smaller pieces and more logic used in a parent component, this would allow for more re-use of similar controls for instance the activity description at the top of the activity page is actually duplicated code, but could be split off into it's own component, and added to each "view" of the page.  This same component could then also be used on the activity list page with a small change to hide the description, since the components are essentially the same.



# Default Angular Commands to help with development and deployment
This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 10.0.4.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `--prod` flag for a production build.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).
