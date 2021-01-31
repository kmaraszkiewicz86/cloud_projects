import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { PhotoAwsModule } from './photo-aws/photo-aws.module'

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    PhotoAwsModule
    
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
