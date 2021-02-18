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
  pageModel;
  p: number = 1;
  userName: string;
  displayedColumns: string[] = ['userName', 'userEmail', 'status', 'buttons'];
  dataSource: MatTableDataSource<IClients>;

  constructor(private store: Store<IAppState>, public dialog: MatDialog) {
    this.dataSource = new MatTableDataSource(this.clientsData);
  }

  ngOnInit(): void {
    this.pageModel = {
      pageParameters: {
        pageNumber: 1,
        pageSize: 5,
      },
      isDescending: false,
      orderByString: '',
      email: '',
      name: '',
    };
    this.store.dispatch(getClients(this.pageModel));
    this.store.pipe(select(selectAdministrator)).subscribe(
      data => {
        this.clientsData = data;
        this.dataSource = new MatTableDataSource(this.clientsData);
        console.log(data);
        console.log(this.clientsData);
      }
    )
  }

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
debugger;
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
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
    this.store.dispatch(getClients(this.pageModel));
    this.store.pipe(select(selectAdministrator)).subscribe(
      data => {
        debugger;
        this.clientsData = data;
        this.dataSource = new MatTableDataSource(this.clientsData);
        console.log(data);
        console.log(this.clientsData);
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
    
    //this.store.dispatch(editClient({ client }))
  }
}
