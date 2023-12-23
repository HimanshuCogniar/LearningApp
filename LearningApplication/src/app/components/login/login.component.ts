import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { LearningservicesService } from 'src/app/services/learningservices.service';
import notify from 'devextreme/ui/notify';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private router: Router,private service: LearningservicesService) {
    this.loginForm = this.formBuilder.group({
      emailNumber: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const reqData = {
        "UserNameOrEmail": this.loginForm.get("emailNumber")?.value,
        "Password": this.loginForm.get("password")?.value
      };

      console.log('Login data:', this.loginForm.value);
      this.service.loginUser(reqData).subscribe((result: any) => {
        if (result.resultCode == 1000) {
        this.router.navigateByUrl('/dashboard');
        this.showLoginToast();
        }
        else{
           this.showLoginError();
        }
      });  


      this.router.navigateByUrl('/dashboard');
    }
  }
  
showLoginToast() {
  notify({
   // message: this.apimessage,
    message: 'User Loggedin successfully',
    isVisible: true,
    displayTime: 2000,
    height: 50,
    type: 'success',
  });
}
showLoginError() {
  notify({

    message: 'Invalid User name or Password',
    isVisible: true,
    displayTime: 5000,
    height: 50,
    width: 5000,
    type: 'Error',
  });

}
}
