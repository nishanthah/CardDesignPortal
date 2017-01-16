import { Component } from '@angular/core';
import { CommonService } from './common.service';
import { Router } from '@angular/router';

@Component({
    selector: 'final',
    templateUrl: 'app/final.component.html',

})

export class FinalComponent {

    constructor(
        private router: Router,
        private commonService: CommonService
    ) { }

    onHomeClick() {
        this.commonService.setCardHolderName("");
        this.commonService.setCardTemplateUrl("");
        this.router.navigate(['/welcome']);
    }
}