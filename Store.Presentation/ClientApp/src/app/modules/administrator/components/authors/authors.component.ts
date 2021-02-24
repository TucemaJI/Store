import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { select, Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { IAuthor } from '../../../shared/models/IAuthor';
import { IPageParameters } from '../../../shared/models/IPageParameters';
import { deleteAuthor, getAuthors } from '../../store/administrator.actions';
import { selectAdministrator } from '../../store/administrator.selector';
import { EditAuthorComponent } from '../edit-author/edit-author.component';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css']
})
export class AuthorsComponent implements OnInit {

  displayedColumns: string[] = ['ID', 'Name', 'Product'];
  pageModel: any;
  pageParameters: IPageParameters;
  authorsData: IAuthor[];
  serchTest: any;

  constructor(private store: Store<IAppState>, public dialog: MatDialog) { }

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
      id: null,
      name: '',
    };
    debugger;
    this.store.dispatch(getAuthors(this.pageModel));
    this.getAuthors();
  }

  getAuthors() {
    this.store.pipe(select(selectAdministrator)).subscribe(

      data => {
        if (data.authors != null && data.pageModel != null) {
          this.authorsData = data.authors;

          this.pageParameters = data.pageModel.pageParameters;
          console.log(data);
        }
      }
    )
  }
  addAuthor() {
    const dialog = this.dialog.open(EditAuthorComponent, {
      data: { addForm: true }
    })
  }
  pageChanged(event) { }
  applyFilter() { }
  reverse() { }
  editAuthor(element) {
    const dialog = this.dialog.open(EditAuthorComponent, {
      data: { addForm: false, element }
    })
  }
  deleteAuthor(element) {
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
