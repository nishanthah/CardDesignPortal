import { Injectable } from '@angular/core';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map'
@Injectable()
export class userService {
    apiUrl:string="http://localhost:4040/"
    constructor(private http: Http) { }
    UserData(fName, lName, code, phoneNum, emailAddress, address, town, cif, dob, accBranch,file) {
               return this.http.post('/api/UserDetails', {"fName":fName,"lName":lName,"code":code,"phoneNum":phoneNum,"emailAddress":emailAddress,"address":address,"town":town,"cif":cif,"dob":dob,"accBranch":accBranch,"file":file})
            .map((response: Response) => {
                         let user = response.json();
                                 if (user) {
                                      // alert("Insert Sucessful");
                    localStorage.setItem('currentUser', JSON.stringify(user));
                }
            })
            .subscribe();
    }

getUser(uId) {

      return this.http.post("api/UserDetails/id", { Id: uId }).toPromise()
          .then(function (response) {
              var result = response.json();
              return result;
          })
  }


getAUser(uId) {

      return this.http.post(this.apiUrl+"api/UserDetails/id", { Id: uId }).toPromise()
          .then(function (response) {
              var result = response.json();
              return result;
          })
  }


    logout() {
               localStorage.removeItem('currentUser');
    }
}
