import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrintingEditionComponent } from './components/printing-edition/printing-edition.component';
import { MaterialModule } from '../shared/material.module';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgxSliderModule } from '@angular-slider/ngx-slider';
import { EnumToArray } from '../shared/services/enum-to-array';
import { SelectPEComponent } from './components/select-pe/select-pe.component';



@NgModule({
  declarations: [PrintingEditionComponent, EnumToArray, SelectPEComponent,],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
    NgxPaginationModule,
    NgxSliderModule,
  ],
  exports: [PrintingEditionComponent, SelectPEComponent,]
})
export class PrintingEditionModule { }
