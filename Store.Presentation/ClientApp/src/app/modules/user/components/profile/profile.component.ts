import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngxs/store';
import { AccountHttpService } from 'src/app/modules/shared/services/account-http.service';
import { ImageFromUrlService } from 'src/app/modules/shared/services/image-from-url.service';
import { EditUser, GetUser } from '../../../account/store/account.actions';
import { IUser } from '../../../shared/models/IUser.model';
import { AuthService } from '../../../shared/services/auth.service';
import { CheckerErrors } from '../../../shared/validator';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  public profileForm: FormGroup;

  public user: IUser;
  public editbool: Boolean;
  public userId: string;

  public image: any;

  constructor(private auth: AuthService, private store: Store, private imageService: ImageFromUrlService, private accService: AccountHttpService) { }

  ngOnInit(): void {
    this.userId = this.auth.getId();
    this.auth.userIdChanged.subscribe((id) => this.userId = id);

    this.store.dispatch(new GetUser(this.userId));
    this.getUserForm();
    this.profileForm = new FormGroup({
      firstName: new FormControl({ value: "", disabled: !this.editbool }, [Validators.required, CheckerErrors.checkFirstSpace]),
      lastName: new FormControl({ value: "", disabled: !this.editbool }, [Validators.required, CheckerErrors.checkFirstSpace]),
      email: new FormControl({ value: "", disabled: !this.editbool }, [Validators.required, Validators.email]),
      password: new FormControl('', [Validators.required, CheckerErrors.checkUpperCase, CheckerErrors.checkSpecialSymbol,
      CheckerErrors.checkLength, CheckerErrors.checkNum]),
      confirmPassword: new FormControl('', [Validators.required]),
    }, { validators: CheckerErrors.checkPasswords });

    this.imageService.getBase64ImageFromURL(this.auth.getPhoto()).subscribe(base64data => {
      this.image = base64data;
    });
  }

  getUserForm(): void {
    this.store.subscribe(
      data => {
        if (data.account.user != null) {
          this.user = data.account.user;
        }
      }
    )
  }

  public hasError = (controlName: string, errorName: string): boolean => {
    return this.profileForm.controls[controlName].hasError(errorName);
  }

  public editButton(): void {
    this.editbool = true;
    CheckerErrors.edit(this.profileForm);
  }

  public cancel(): void {
    location.reload();
  }

  public save(profileFormValue: IUser): void {
    const user: IUser = {
      confirmPassword: profileFormValue.confirmPassword,
      email: profileFormValue.email,
      firstName: profileFormValue.firstName.trim(),
      id: this.user.id,
      lastName: profileFormValue.lastName.trim(),
      password: profileFormValue.password,
    }
    this.store.dispatch(new EditUser(user));
    this.editbool = false;
  }

  public createFile(file: FileList) {
    let img = file.item(0);
    const formData = new FormData();
    formData.append('file', img, `${this.user.id}${img.name.substr(img.name.lastIndexOf('.'))}`);
    this.accService.uploadPhoto(formData).subscribe(
      () => {
        debugger;
      }
    );
  }

}
