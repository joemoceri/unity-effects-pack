import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { Router } from '@angular/router';
//import { createUnityInstance } from '../assets/demo/Build/WebGL.loader.js';
 
@Component({
    selector     : 'demo',
    templateUrl  : './demo.component.html',
    styleUrls    : ['./demo.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class DemoComponent implements OnInit
{
    constructor(
        private router: Router
    )
    {
        
    }

  async ngOnInit() {
    //const result = UnityLoader.instantiate("unityContainer", "assets/demo/Build/demo.json");

    //console.log(result);
  }
}
