import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
// import { MatCheckboxModule } from '@angular/material/checkbox';
// import { MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatIconModule, MatInputModule, MatCardModule, MatDividerModule } from '@angular/material';
// import { MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatIconModule, MatInputModule, MatCardModule, MatDividerModule } from '@angular/material';
// import { MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatIconModule, MatInputModule, MatCardModule, MatDividerModule } from '@angular/material';
// import { MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatIconModule, MatInputModule, MatCardModule, MatDividerModule } from '@angular/material';
// import { MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatIconModule, MatInputModule, MatCardModule, MatDividerModule } from '@angular/material';
// import { MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatIconModule, MatInputModule, MatCardModule, MatDividerModule } from '@angular/material';

import { AboutComponent } from './about.component';
import { FlexLayoutModule } from '@angular/flex-layout';
const routes: Routes = [
    {
        path     : '',
        component: AboutComponent
    },
    //{
    //    path: '', redirectTo: 'about', pathMatch: 'full' // default: redirect to about when public
    //},
];

@NgModule({
    declarations: [
        AboutComponent
    ],
    imports     : [
        RouterModule.forChild(routes),

        MatButtonModule,
        // MatCheckboxModule,
        // MatFormFieldModule,
        MatIconModule,
        // MatInputModule,
        // MatCardModule,
      MatDividerModule,
      FlexLayoutModule
    ]
})
export class AboutModule
{
}
