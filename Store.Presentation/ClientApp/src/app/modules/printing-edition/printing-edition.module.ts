import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PrintingEditionComponent } from './components/printing-edition/printing-edition.component';
import { MaterialModule } from '../shared/material.module';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { NgxSliderModule } from '@angular-slider/ngx-slider';



@NgModule({
  declarations: [PrintingEditionComponent],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
    NgxPaginationModule,
    NgxSliderModule,
  ],
  exports: [PrintingEditionComponent,]
})
export class PrintingEditionModule { }
