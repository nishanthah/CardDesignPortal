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

    userData(fName, lName, code, phoneNum, emailAddress, address, town, cif, dob, accBranch, file): Observable<boolean> {
        var currentObj = this;
        return this.http.post(this.commonService.getApiHostAddress() + 'api/User', {
            "Id": this.commonService.getTokenUserId(),
            "FirstName": fName,
            "LastName": lName,
            "PhoneNumber": phoneNum,
            "EmailAddress": emailAddress,
            "MaillingAddress": address,
            "Town": town,
            "Cif": cif,
            "DateOfBirth": dob,
            "AccountBranch": accBranch,
            "Image": file
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

        return this.http.post(
            this.commonService.getApiHostAddress() + "api/UserById",
            { "id": uId },
            this.commonService.getHeaderOption()
        ).toPromise()
            .then(function (response) {
                var result = response.json();
                return result;
            })
    }

    isUserRegister(id: number) {

        return this.http.post(
            this.commonService.getApiHostAddress() + "api/IsUserRegister",
            { "id": id },
            this.commonService.getHeaderOption()
        ).toPromise()
            .then(function (response) {
                var result = response.json();
                return result;
            })
    }

    logout() {
        this.commonService.setToken("");
        this.commonService.setCardTemplateUrl("");
        this.commonService.setCardHolderName("");
    }
}
