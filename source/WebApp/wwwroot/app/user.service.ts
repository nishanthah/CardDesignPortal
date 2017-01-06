import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';

import 'rxjs/add/operator/map'

import { CommonService } from './common.service';

@Injectable()
export class UserService {

    constructor(
        private http: Http,
        private commonService: CommonService
    ) { }

    UserData(fName, lName, code, phoneNum, emailAddress, address, town, cif, dob, accBranch, file) : Observable<boolean>{
        var currentObj = this;
        return this.http.post(this.commonService.getApiHostAddress() + 'api/UserDetails', {
            "fName": fName,
            "lName": lName,
            "code": code,
            "phoneNum": phoneNum,
            "emailAddress": emailAddress,
            "address": address,
            "town": town,
            "cif": cif,
            "dob": dob,
            "accBranch": accBranch,
            "file": file
        },
            this.commonService.getHeaderOption()
        ).map((response: Response) => {            
            let user = response.json();
            return user;
        }).catch((error: Response | any) => {
            let errMsg: string;
            if (error instanceof Response) {
                const body = error.json() || '';
                const err = body.error || JSON.stringify(body);
                errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
            } else {
                errMsg = error.message ? error.message : error.toString();
            }
            console.error(errMsg);
            return Observable.throw(errMsg);            
        });

    }

    getUser(uId) {

        return this.http.post("api/UserDetails/id", { Id: uId }).toPromise()
            .then(function (response) {
                var result = response.json();
                return result;
            })
    }

    getAUser(uId) {

        return this.http.post(this.commonService.getApiHostAddress() + "api/UserDetails/id", { Id: uId }).toPromise()
            .then(function (response) {
                var result = response.json();
                return result;
            })
    }

    logout() {
        localStorage.removeItem('currentUser');
    }
}
