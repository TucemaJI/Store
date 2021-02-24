import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Store } from '@ngrx/store';
import { IAppState } from 'src/app/store/state/app.state';
import { isUndefined } from 'util';
import { addAuthor, changeAuthor } from '../../store/administrator.actions';

@Component({
  selector: 'app-edit-author',
  templateUrl: './edit-author.component.html',
  styleUrls: ['./edit-author.component.css']
})
export class EditAuthorComponent implements OnInit {

  addForm: boolean;
  form: FormGroup;
  constructor(public dialogRef: MatDialogRef<EditAuthorComponent>, @Inject(MAT_DIALOG_DATA) public data, private store: Store<IAppState>) { }

  ngOnInit(): void {
    this.addForm = this.data.addForm;
    debugger;
    if (this.addForm) {
      this.form = new FormGroup({
        firstName: new FormControl(),
        lastName: new FormControl(),
      });
    }
    if (!this.addForm) {
      this.form = new FormGroup({
        firstName: new FormControl(this.data.element.firstName,),
        lastName: new FormControl(this.data.element.lastName,),
      });
    }
  }

  submit(formValue) {
    debugger;
    let author = {
      firstName: formValue.firstName,
      lastName: formValue.lastName,
      id: null,
      printingEditions: null,
    }
    debugger;
    if (this.addForm) {
      this.store.dispatch(addAuthor({ author }));
    }
    if (!this.addForm) {
      author.id = this.data.element.id;
      this.store.dispatch(changeAuthor({ author }));
    }
    location.reload();
  }
  cancel() {
    this.dialogRef.close();
  }
}
