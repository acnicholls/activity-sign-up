'use strict';

import { HttpHeaders } from '@angular/common/http';
import { environment } from './../environments/environment';

export const Globals = Object.freeze({
    DATA_ACCESS_PREFIX:environment.apiUrl,
    COOKIE_NAME: 'activity-sign-up'
});
 