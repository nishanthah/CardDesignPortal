import {Injectable} from '@angular/core';
import {Http, Response} from '@angular/http';
import {Headers, RequestOptions} from '@angular/http';
import {CardRequest} from './Request-Card';
import {Observable}     from 'rxjs/Observable';



@Injectable()
export class ResetPasswordService {
    /*
  constructor(public _http: Http) { }
  private _cardDetailsUrl = 'api/CardRequest';

  SendEmail(email) {
      return this._http.post("api/CardRequest/id", { Email: email }).toPromise()
          .then(function (response) {
              var result = response.json();
              return result;
          })
  }
  */
    apiUrl:string="http://localhost:4040/"
    public recipient: string="smithnishan4n@gmail.com";
    public subject: string="Sub";
    public message: string="Your reset code";
    private mailgunUrl: string = "MAILGUN_URL_HERE";
    private apiKey: string = "1e168fd438b97e365466af134ee69c16";
 
    public constructor(private _http: Http) {
 
    }
 
    public sendResetCode(email)
    {
        return this._http.post(this.apiUrl+"api/Reset/id", { emailAddress: email }).toPromise()
          .then(function (response) {
              var result = response.json();
              return result;
          })
    }
    
    public ResetPassword(email,newResetCode,newPassword)
    {
        
        return this._http.post(this.apiUrl+"api/Password/id", { emailAddress: email,resetCode:newResetCode,password:newPassword }).toPromise()
          .then(function (response) {
              var result = response.json();
              return result;
          })
    }
    public send() {
        /*
        if(this.recipient && this.subject && this.message) {
            let headers = new Headers(
                {
                    "Content-Type": "application/x-www-form-urlencoded",
                    "Authorization": "Basic " + this.apiKey
                }
            );
            let options = new RequestOptions({ headers: headers });
            let body = "from=angular2ntbdemo@gmail.com&to=" + this.recipient + "&subject=" + this.subject + "&text=" + this.message;
            this._http.post("https://api.mailgun.net/v3/" + this.mailgunUrl + "/messages", body, options)
                .map(result => result.json())
                .do(result => console.log("RESULT: ", JSON.stringify(result)))
                .subscribe(result => {
                    console.log("SENT!");
                    this.recipient = "";
                    this.subject = "";
                    this.message = "";
                }, error => {
                    console.log(error);
                });
        }
        */
    }
  
}

