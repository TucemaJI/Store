import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { select, Store } from '@ngrx/store';
import { Consts } from 'src/app/modules/shared/consts';
import { IAuthorsPage } from 'src/app/modules/shared/models/IAuthorsPage.model';
import { IAppState } from 'src/app/store/state/app.state';
import { IAuthor } from '../../../shared/models/IAuthor.model';
import { IPageOptions } from '../../../shared/models/IPageOptions.model';
import { deleteAuthor, getAuthors } from '../../store/administrator.actions';
import { selectAdministrator } from '../../store/administrator.selector';
import { EditAuthorComponent } from '../edit-author/edit-author.component';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css']
})
export class AuthorsComponent implements OnInit {

  displayedColumns: string[] = Consts.AUTHOR_COLUMNS;
  pageModel: IAuthorsPage;
  pageParameters: IPageOptions;
  authorsData: IAuthor[];
  searchText: string;
  isDescending: boolean = false;

  constructor(private store: Store<IAppState>, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.pageParameters = Consts.AUTHOR_PAGE_PARAMETERS;
    this.pageModel = {
      pageOptions: this.pageParameters,
      isDescending: this.isDescending,
      orderByString: '',
      id: null,
      name: '',
    };
    this.store.dispatch(getAuthors({ pageModel: this.pageModel }));
    this.getAuthors();
  }

  getAuthors(): void {
    this.store.pipe(select(selectAdministrator)).subscribe(

      data => {
        if (data.authors != null && data.pageModel != null) {
          this.authorsData = data.authors;

          this.pageParameters = data.pageModel.pageOptions;
        }
      }
    )
  }

  addAuthor(): void {
    const dialog = this.dialog.open(EditAuthorComponent, {
      data: { addForm: true }
    })
  }

  pageChanged(event: number): void {
    this.pageModel.pageOptions = { currentPage: event, itemsPerPage: this.pageParameters.itemsPerPage, totalItems: this.pageParameters.totalItems, };
    this.store.dispatch(getAuthors({ pageModel: this.pageModel }));
  }

  applyFilter(event, filterName: string): void {
    this.pageModel = {
      pageOptions: this.pageParameters,
      isDescending: this.isDescending,
      orderByString: filterName,
      id: null,
      name: '',
    };
    if (filterName === Consts.NAME) {
      this.pageModel.name = event;
    };
    if (filterName === Consts.ID) {
      this.pageModel.id = event;
    }
    this.store.dispatch(getAuthors({ pageModel: this.pageModel }));
  }

  reverse(orderBy: string): void {
    this.isDescending = this.isDescending === true ? this.isDescending = false : this.isDescending = true;
    this.pageModel.isDescending = this.isDescending;
    this.pageModel.orderByString = orderBy;
    this.store.dispatch(getAuthors({ pageModel: this.pageModel }));
  }

  editAuthor(element: IAuthor): void {
    const dialog = this.dialog.open(EditAuthorComponent, {
      data: { addForm: false, element }
    })
  }

  deleteAuthor(element: IAuthor): void {
    const author: IAuthor = {
      id: element.id,
      firstName: element.firstName,
      lastName: element.lastName,
      printingEditions: null,
    }
    this.store.dispatch(deleteAuthor({ author }));
    location.reload();
  }
}
