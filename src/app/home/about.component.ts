import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Router } from '@angular/router';

@Component({
    selector     : 'about',
    templateUrl  : './about.component.html',
    styleUrls    : ['./about.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class AboutComponent implements OnInit
{
    constructor(
        private router: Router
    )
    {
        
    }

    ngOnInit(): void
    {

    }
}