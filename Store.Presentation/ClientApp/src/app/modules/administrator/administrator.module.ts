import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdministratorComponent } from './components/administrator/administrator.component';
import { ClientsComponent } from './components/clients/clients.component';
import { MaterialModule } from '../shared/material.module';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';



@NgModule({
  declarations: [AdministratorComponent, ClientsComponent],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
    NgxPaginationModule,
  ],
  exports: [AdministratorComponent, ClientsComponent]
})
export class AdministratorModule { }
