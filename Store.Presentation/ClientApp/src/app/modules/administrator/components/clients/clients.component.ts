import { isNull } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgControl, NgModel } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { select, Store } from '@ngrx/store';
import { AdministratorHttpService } from 'src/app/modules/shared/services/administrator-http.service';
import { IAppState } from 'src/app/store/state/app.state';
import { IChangeClientModel } from '../../../shared/models/IChangeClientModel';
import { IClients } from '../../../shared/models/IClients';
import { IClientsPageModel } from '../../../shared/models/IClientsPageModel';
import { IPageParameters } from '../../../shared/models/IPageParameters';
import { clientChange, deleteClient, getClients } from '../../store/administrator.actions';
import { selectAdministrator } from '../../store/administrator.selector';
import { EditProfileComponent } from '../edit-profile/edit-profile.component';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css'],
})
export class ClientsComponent implements OnInit {
  clientsData: IClients[];
  searhText: string;
  statusFilter: boolean;
  pageModel: any;
  pageParameters: IPageParameters;
  displayedColumns: string[] = ['userName', 'userEmail', 'status', 'buttons'];

  constructor(private store: Store<IAppState>, public dialog: MatDialog, private http: AdministratorHttpService) {
  }

  ngOnInit(): void {
    this.pageParameters = {
      itemsPerPage: 5,
      currentPage: 1,
      totalItems: 0,
    }
    this.pageModel = {
      pageParameters: this.pageParameters,
      isDescending: false,
      orderByString: '',
      email: '',
      name: '',
    };
    //this.http.postClientsPage(this.pageModel).subscribe(data => console.log(data));
    this.store.dispatch(getClients(this.pageModel));
    this.getClients();
  }

  filterSlide() {
    this.pageModel.isBlocked = this.statusFilter;
    this.store.dispatch(getClients(this.pageModel));
  }

  pageChanged(event) {
    this.pageModel.pageParameters = { currentPage: event, itemsPerPage: this.pageParameters.itemsPerPage, totalItems: this.pageParameters.totalItems, };
    this.store.dispatch(getClients(this.pageModel));
  }

  applyFilter(event, filterName: string) {
    this.pageModel = {
      pageParameters: this.pageParameters,
      isDescending: false,
      orderByString: filterName,
      email: '',
      name: '',
      isBlocked: this.statusFilter,
    };
    if (filterName === 'userName') {
      this.pageModel.name = event;
    };
    if (filterName === 'email') {
      this.pageModel.email = event;
    }
    this.store.dispatch(getClients(this.pageModel));
  }
  changeUserBlock(element) {
    let client: IChangeClientModel = {
      firstName: element.firstName,
      lastName: element.lastName,
      email: element.email,
      isBlocked: element.isBlocked,
      password: element.password,
      confirmPassword: element.confirmPassword,
      id: element.id,
    };
    if (element.isBlocked) {
      client.isBlocked = false;
    }
    if (!element.isBlocked) {
      client.isBlocked = true;
    }
    this.store.dispatch(clientChange({ client }));
    location.reload();
  }
  getClients() {
    this.store.pipe(select(selectAdministrator)).subscribe(

      data => {
        if (data.clients != null && data.pageModel != null) {
          this.clientsData = data.clients;

          this.pageParameters = data.pageModel.pageParameters;
          console.log(data);
        }
      }
    )
  }
  deleteClient(element) {
    const client: IClients = {
      firstName: element.firstName,
      lastName: element.lastName,
      email: element.email,
      isBlocked: element.isBlocked,
    };
    this.store.dispatch(deleteClient({ client }));
    location.reload();
  }
  editClient(element) {
    const client = {
      firstName: element.firstName,
      lastName: element.lastName,
      email: element.email,
      isBlocked: element.isBlocked,
      id: element.id,
    };
    const dialog = this.dialog.open(EditProfileComponent, {
      data: client,
    })
  }
}
