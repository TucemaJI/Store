import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdministratorComponent } from './components/administrator/administrator.component';
import { ClientsComponent } from './components/clients/clients.component';
import { MaterialModule } from '../shared/material.module';
import { RouterModule } from '@angular/router';
import { NgxPaginationModule } from 'ngx-pagination';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';
import { AuthorsComponent } from './components/authors/authors.component';



@NgModule({
  declarations: [AdministratorComponent, ClientsComponent, EditProfileComponent, AuthorsComponent],
  imports: [
    CommonModule,
    MaterialModule,
    RouterModule,
    NgxPaginationModule,
  ],
  exports: [AdministratorComponent, ClientsComponent]
})
export class AdministratorModule { }
