import { Injectable } from '@angular/core';
import { Http, Headers, Response, URLSearchParams, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { CommonService } from './common.service';

@Injectable()
export class AuthenticationService {
    constructor(private http: Http, private common: CommonService) { }

    login(username, password) {

        let urlSearchParams = new URLSearchParams();
        urlSearchParams.append('username', username);
        urlSearchParams.append('password', password);

        let body = urlSearchParams.toString();
        let headers = new Headers({ 'Content-Type': 'application/x-www-form-urlencoded' });
        let options = new RequestOptions({ headers: headers });
        return this.http.post(this.common.getHostAddress() + "api/Authenticity", body, options).toPromise()
            .then(function (response) {
                var result = response.json();
                return result;
            })
            .catch(function () {
                return null;
            });
    };

    logout() {
        // remove user from local storage to log user out
        localStorage.removeItem('currentUser');
    }
}