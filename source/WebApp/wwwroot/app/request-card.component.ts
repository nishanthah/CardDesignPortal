import { Component, Directive, AfterViewInit } from '@angular/core';
import { Router } from '@angular/router';
import { RequestCardModel } from './RequestCardModel';
import { CommonService } from './common.service';

declare var componentHandler: any;

@Component({
    selector: 'request-card',
    templateUrl: 'app/request-card.component.html',

})

export class RequestCardComponent implements AfterViewInit {

    public active: boolean = false;
    public requestCardModel: RequestCardModel;

    public imageList: string[] = [
        '../images/1.jpg',
        '../images/2.jpg',
        '../images/3.jpg',
        '../images/4.jpg',
        '../images/5.jpg',
        '../images/6.jpg',
    ];

    constructor(private router: Router, private commonService: CommonService) {
        this.requestCardModel = new RequestCardModel();
        this.requestCardModel.cardTemplateUrl = this.imageList[0];
    }
    ngAfterViewInit() {
        componentHandler.upgradeAllRegistered();
    }
    onSubmit(cardHolderName: string) {
        if (cardHolderName == null) {
            this.active = true;
        } else if (cardHolderName.length == 0) {
            this.active = true;
        }
        else {
            this.commonService.setCardHolderName(cardHolderName);
            this.commonService.setCardTemplateUrl(this.requestCardModel.cardTemplateUrl);
            this.router.navigate(['/card_detail']);
        }
    }
    onImageClick(image) {
        this.requestCardModel.cardTemplateUrl = this.imageList[image];
    }
    onTextChange(cardHolderName: string) {
        if (cardHolderName == null) {
            this.active = true;
        } else if (cardHolderName.length == 0) {
            this.active = true;
        } else {
            this.active = false;
        }
    }
}