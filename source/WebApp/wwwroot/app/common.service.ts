import { Injectable } from "@angular/core";

@Injectable()
export class CommonService {

    private cardHolderName: string;
    private cardTemplateUrl: string;
    private userToken: string;
    private hostAddress: string;

    constructor() {
        this.hostAddress = "http://localhost:4040/";
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

    getHostAddress() {
        return this.hostAddress;
    }
}