import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router'
import { FormsModule } from '@angular/forms';

import { DynamoDbListComponent } from './dynamo-db-list/dynamo-db-list.component';
import { DynamoDbAddComponent } from './dynamo-db-add/dynamo-db-add.component';
import { PhotoAwsComponent } from './photo-aws.component';
import { DynamoDbDetailsComponent } from './dynamo-db-details/dynamo-db-details.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forRoot([
      { path: 'app-dynamo-db-details/:id', component: DynamoDbDetailsComponent },
      { path: 'photo-list-aws', component: PhotoAwsComponent },
      { path: '', redirectTo: 'photo-list-aws', pathMatch: 'full' },
      { path: '**', redirectTo: 'photo-list-aws', pathMatch: 'full'}
    ])
  ],
  declarations: [
    DynamoDbListComponent,
    DynamoDbAddComponent,
    PhotoAwsComponent,
    DynamoDbDetailsComponent
  ]
})
export class PhotoAwsModule { }
