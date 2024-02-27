import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-microfrontend',
  templateUrl: './microfrontend.component.html',
  styleUrls: ['./microfrontend.component.css']
})
export class MicrofrontendComponent {
  @Input() signupinput:any;
  @Output() signupoutput=new EventEmitter<String>;
  signup(){
    console.log("signup");
   this.signupoutput.emit(this.signupinput);
  }
}
