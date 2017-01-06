import { Injectable } from "@angular/core";
import { Headers, RequestOptions } from '@angular/http';

@Injectable()
export class CommonService {

    private cardHolderName: string;
    private cardTemplateUrl: string;
    private userToken: string;
    private apihostAddress: string;
    private webAppHostAddress: string;

    constructor() {
        this.apihostAddress = "http://localhost:4040/";
        this.webAppHostAddress = "http://localhost:5000/";
    }

    setCardHolderName(name: string) {
        this.cardHolderName = name;
    }

    getCardHolderName() {
        return this.cardHolderName;
    }

    setCardTemplateUrl(url: string) {
        this.cardTemplateUrl = url;
    }

    getCardTemplateUrl() {
        return this.cardTemplateUrl;
    }

    setToken(token: string) {
        this.userToken = token;
    }

    getToken() {
        return "Bearer " + this.userToken;
    }

    getApiHostAddress() {
        return this.apihostAddress;
    }

    getHeaderOption() {
        let headers = new Headers({ 'Authorization': this.getToken() });
        let options = new RequestOptions({ headers: headers });
        return options;
    }
}