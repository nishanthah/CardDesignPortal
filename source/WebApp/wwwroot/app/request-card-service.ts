import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { CardRequest } from './Request-Card';
import { Observable } from 'rxjs/Observable';
import { CommonService } from './common.service';

@Injectable()
export class RequestCardService {

    constructor(
        private _http: Http,
        private commonService: CommonService
    ) { }

    private _cardDetailsUrl = 'api/CardRequest';
    private cardDetailsUrl = 'api/CardRequest';

    getRequestCardDetails(): Observable<CardRequest[]> {
        return this._http.get(this._cardDetailsUrl)
            .map((response: Response) => <CardRequest[]>response.json())
            //.do(data => console.log("All :" + JSON.stringify(data)));
            .catch(this.handleError);

    }

    CardRequest(id, templateId, cardHolderName, expireDate) {

        return this._http.post(
            this.commonService.getApiHostAddress() + this.cardDetailsUrl,
            {
                "Id": id,
                "TemplateId": templateId,
                "CardHolderName": cardHolderName,
                "ExpireDate": expireDate
            },
            this.commonService.getHeaderOption()
        )
            .map((response: Response) => {
                let responseValue = response.json();
                return responseValue;
            })
            .catch((error: Response | any) => {
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
            })

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

