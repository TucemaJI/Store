import { isNull } from '@angular/compiler/src/output/output_ast';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { select, Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { IChangeModel } from '../../models/IChangeModel';
import { IClients } from '../../models/IClients';
import { IPageModel } from '../../models/IPageModel';
import { IPageParameters } from '../../models/IPageParameters';
import { clientChange, deleteClient, getClients } from '../../store/administrator.actions';
import { selectAdministrator } from '../../store/administrator.selector';
import { EditProfileComponent } from '../edit-profile/edit-profile.component';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css']
})
export class ClientsComponent implements OnInit {
  clientsData: IClients[];
  pageModel: any;
  pageParameters: IPageParameters;
  displayedColumns: string[] = ['userName', 'userEmail', 'status', 'buttons'];

  constructor(private store: Store<IAppState>, public dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.pageModel = {
      pageParameters: {
        itemsPerPage: 5,
        currentPage: 1,
      },
      isDescending: false,
      orderByString: '',
      email: '',
      name: '',
    };
    this.store.dispatch(getClients(this.pageModel));
    this.getClients();


  }

  pageChanged(event) {
    debugger;
    this.pageModel.currentPage = event;
  }

  applyFilter(value: string, filterName: string) {
    debugger;
    this.pageModel = {
      pageParameters: this.pageParameters,
      isDescending: false,
      orderByString: filterName,
      email: '',
      name: '',
    };
    if (filterName === 'userName') {
      this.pageModel.name = value;
    };
    if (filterName === 'email') {
      this.pageModel.email = value;
    }
    debugger;
    this.store.dispatch(getClients(this.pageModel));
    debugger;
  }
  changeUserBlock(element) {
    debugger;
    let client: IChangeModel = {
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
          debugger;
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
