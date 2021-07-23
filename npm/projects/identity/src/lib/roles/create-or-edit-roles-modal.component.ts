import { IdentityRoleCreateDto, IdentityRoleDto, IdentityRoleService, IdentityRoleUpdateDto } from '@abp/ng.identity';
import { Component, Input, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalRef } from 'ng-zorro-antd/modal';
import { finalize } from 'rxjs/operators';

@Component({
    selector: 'app-create-or-edit-roles-modal',
    templateUrl: './create-or-edit-roles-modal.component.html'
})
export class CreateOrEditRolesModalComponent implements OnInit {

    @Input() parentId: string;
    @Input() id: string;

    saving = false;
    role: IdentityRoleDto = {} as IdentityRoleDto;

    constructor(
        private message: NzMessageService,
        private identityRoleService: IdentityRoleService,
        public modalRef: NzModalRef<CreateOrEditRolesModalComponent>
    ) {
    }

    ngOnInit() {
        if (this.id) {
            this.identityRoleService.get(this.id).subscribe(result => {
                this.role = result;
            });
        }
    }

    save() {
        this.saving = true;
        if (this.id) {
            const input = {
                name: this.role.name,
                isDefault: this.role.isDefault,
                isPublic: this.role.isPublic,
                extraProperties: {
                    parentId: this.parentId
                },
                concurrencyStamp: this.role.concurrencyStamp
            } as IdentityRoleUpdateDto;
            this.identityRoleService.update(this.id, input).pipe(finalize(() => { this.saving = false; })).subscribe(result => {
                this.message.success('保存成功');
                this.modalRef.close(true);
            });
        } else {
            const input = {
                name: this.role.name,
                isDefault: this.role.isDefault,
                isPublic: this.role.isPublic,
                extraProperties: {
                    parentId: this.parentId
                }
            } as IdentityRoleCreateDto;
            this.identityRoleService.create(input).pipe(finalize(() => { this.saving = false; })).subscribe(result => {
                this.message.success('创建成功');
                this.modalRef.close(true);
            });
        }
    }
}
