import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router'
import { HttpClientModule } from '@angular/common/http';

import { DynamoDbListComponent } from './dynamo-db-list/dynamo-db-list.component';
import { DynamoDbAddComponent } from './dynamo-db-add/dynamo-db-add.component';
import { PhotoAwsComponent } from './photo-aws.component';

@NgModule({
  imports: [
    CommonModule,
    RouterModule.forRoot([
      { path: 'photo-list-aws', component: PhotoAwsComponent },
      { path: '', redirectTo: 'photo-list-aws', pathMatch: 'full' },
      { path: '**', redirectTo: 'photo-list-aws', pathMatch: 'full'}
    ])
  ],
  declarations: [
    DynamoDbListComponent,
    DynamoDbAddComponent,
    PhotoAwsComponent
  ]
})
export class PhotoAwsModule { }
