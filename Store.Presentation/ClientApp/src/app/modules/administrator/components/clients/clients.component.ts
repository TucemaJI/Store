import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { select, Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { IClients } from '../../models/IClients';
import { IPageModel } from '../../models/IPageModel';
import { getClients } from '../../store/administrator.actions';
import { selectAdministrator } from '../../store/administrator.selector';

// const clientsData: IClients[] = [
//   { userFirstName: 'Vasiliy', userLastName: 'Zveno', userEmail: 'vasya@gmail.com', status: true }
// ]

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

  constructor(private store: Store<IAppState>) {
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

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

}
