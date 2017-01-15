import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { CardRequest } from './Request-Card';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class RequestCardService {

    constructor(public _http: Http) { }

    private _cardDetailsUrl = 'api/CardRequest';

    getRequestCardDetails(): Observable<CardRequest[]> {
        return this._http.get(this._cardDetailsUrl)
            .map((response: Response) => <CardRequest[]>response.json())
            //.do(data => console.log("All :" + JSON.stringify(data)));
            .catch(this.handleError);

    }

    private cardDetailsUrl = '/api/CardRequest/id';
    RequestCard(id: number): Observable<CardRequest> {

        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this._http.post(this.cardDetailsUrl, { "id": 1 }, options)
            .map(this.extractData)
            .catch(this.handleError).do(data => console.log("one :" + JSON.stringify(data)));
    }

    CardRequest(id, cardNo, templateId, cardHolderName, expireDate) {

        return this._http.post('api/CardRequest', { "id": id, "cardNo": cardNo, "templateId": templateId, "cardHolderName": cardHolderName, "expireDate": expireDate })
            .map((response: Response) => {

                let apiResponse = response.json();
                if (apiResponse)
                    alert("Request had made successfully...!");
                else
                    alert("Error");

            })
            .subscribe();

    }


    private extractData(res: Response) {
        let body = res.json();
        return body.data || {};
    }
    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }

    getACard(uId) {

        return this._http.post("api/CardRequest/id", { Id: uId }).toPromise()
            .then(function (response) {

                var result = response.json();

                return result;
            })
    }


}

