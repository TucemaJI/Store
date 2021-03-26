import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { select, Store } from '@ngrx/store';
import { Consts } from 'src/app/modules/shared/consts';
import { IClientsPage } from 'src/app/modules/shared/models/IClientsPage.model';
import { IAppState } from 'src/app/store/state/app.state';
import { IChangeClient } from '../../../shared/models/IChangeClient.model';
import { IClient as IClient } from '../../../shared/models/IClient.model';
import { IPageOptions } from '../../../shared/models/IPageOptions.model';
import { clientChange, deleteClient, getClients } from '../../store/administrator.actions';
import { selectAdministrator } from '../../store/administrator.selector';
import { EditProfileComponent } from '../edit-profile/edit-profile.component';

@Component({
  selector: 'app-clients',
  templateUrl: './clients.component.html',
  styleUrls: ['./clients.component.css'],
})
export class ClientsComponent implements OnInit {
  clientsData: IClient[];
  searhText: string;
  statusFilter: boolean;
  pageModel: IClientsPage;
  pageParameters: IPageOptions;
  displayedColumns: string[] = Consts.CLIENTS_COLUMNS;

  constructor(private store: Store<IAppState>, public dialog: MatDialog) {}

  ngOnInit(): void {
    this.pageParameters = Consts.CLIENTS_PAGE_PARAMETERS;
    this.pageModel = {
      pageOptions: this.pageParameters,
      isDescending: false,
      isBlocked: null,
      orderByString: '',
      email: '',
      name: '',
    };
    this.store.dispatch(getClients({ pageModel: this.pageModel }));
    this.getClients();
  }

  filterSlide(): void {
    this.pageModel.isBlocked = this.statusFilter;
    this.store.dispatch(getClients({ pageModel: this.pageModel }));
  }

  pageChanged(event: number): void {
    this.pageModel.pageOptions = { currentPage: event, itemsPerPage: this.pageParameters.itemsPerPage, totalItems: this.pageParameters.totalItems, };
    this.store.dispatch(getClients({ pageModel: this.pageModel }));
  }

  applyFilter(event: string, filterName: string): void {
    this.pageModel = {
      pageOptions: this.pageParameters,
      isDescending: false,
      orderByString: filterName,
      email: '',
      name: '',
      isBlocked: this.statusFilter,
    };
    if (filterName === Consts.FILTER_USERNAME) {
      this.pageModel.name = event;
    };
    if (filterName === Consts.FILTER_EMAIL) {
      this.pageModel.email = event;
    }
    this.store.dispatch(getClients({ pageModel: this.pageModel }));
  }

  changeUserBlock(element: IChangeClient): void {
    if (element.isBlocked) {
      element.isBlocked = false;
    }
    if (!element.isBlocked) {
      element.isBlocked = true;
    }
    this.store.dispatch(clientChange({ client: element }));
    location.reload();
  }

  getClients(): void {
    this.store.pipe(select(selectAdministrator)).subscribe(

      data => {
        if (data.clients != null && data.pageModel != null) {
          this.clientsData = data.clients;

          this.pageParameters = data.pageModel.pageOptions;
        }
      }
    )
  }

  deleteClient(element: IClient): void {
    this.store.dispatch(deleteClient({ client: element }));
    location.reload();
  }

  editClient(element: IClient): void {
    const dialog = this.dialog.open(EditProfileComponent, {
      data: element,
    })
  }
}
