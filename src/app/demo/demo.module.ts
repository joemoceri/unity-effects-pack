import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { DemoComponent } from './demo.component';
import { FlexLayoutModule } from '@angular/flex-layout';
const routes: Routes = [
    {
        path     : '',
        component: DemoComponent
    }
];

@NgModule({
    declarations: [
        DemoComponent
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
export class DemoModule
{
}
