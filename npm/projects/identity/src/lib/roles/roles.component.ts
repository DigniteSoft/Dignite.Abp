import { IdentityRoleService } from '@abp/ng.identity';
import { Component, OnInit } from '@angular/core';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzModalService } from 'ng-zorro-antd/modal';
import { finalize } from 'rxjs/operators';
import { IdentityRole, TreeNode } from '../services/organization-tree';
import { OrganizationTreeService } from '../services/organization-tree.service';
import { CreateOrEditRolesModalComponent } from './create-or-edit-roles-modal.component';
import { ePermissionManagementComponents } from '@abp/ng.permission-management';

@Component({
  selector: 'app-roles',
  templateUrl: './roles.component.html'
})
export class RolesComponent implements OnInit {

  isLoading = false;
  result: Array<TreeNode<IdentityRole>>;

  providerKey = '';
  permissionManagementKey = ePermissionManagementComponents.PermissionManagement;
  visiblePermissions = false;

  onVisiblePermissionChange = event => {
    this.visiblePermissions = event;
  };

  constructor(
    private modalService: NzModalService,
    private messageService: NzMessageService,
    private identityRoleService: IdentityRoleService,
    private organizationTreeService: OrganizationTreeService<IdentityRole>
  ) {
    this.result = [];
  }

  ngOnInit() {
    this.getList();
  }

  getList() {
    this.identityRoleService.getList({
      skipCount: 0,
      maxResultCount: 1000
    }).pipe(finalize(() => { this.isLoading = false; })).subscribe(result => {
      const items = result.items.map(m => {
        const n: IdentityRole = m;
        n.parentId = n.extraProperties.ParentId;
        return n;
      });
      this.result = this.organizationTreeService.getTree(items, false);
    });
  }

  create(treeNode: TreeNode<IdentityRole> = {} as TreeNode<IdentityRole>) {
    const modal = this.modalService.create({
      nzTitle: treeNode.key ? `新建${treeNode.title}下级岗位` : '新建顶级岗位',
      nzContent: CreateOrEditRolesModalComponent,
      nzComponentParams: {
        parentId: treeNode.key
      }
    });

    modal.afterClose.subscribe((result: IdentityRole) => {
      if (result) {
        if (!treeNode.key) {
          this.ngOnInit();
        } else if (treeNode.expand) {
          treeNode.children.unshift(this.organizationTreeService.getTreeNode(result, treeNode.level + 1, false));
        } else {
          treeNode.children.unshift(this.organizationTreeService.getTreeNode(result, treeNode.level + 1, false));
          this.expandChange(true, treeNode);
        }
      }
    });
  }

  edit(treeNode: TreeNode<IdentityRole>) {
    const modal = this.modalService.create({
      nzTitle: `编辑组织：${treeNode.title}`,
      nzContent: CreateOrEditRolesModalComponent,
      nzComponentParams: {
        id: treeNode.key,
        parentId: treeNode.parentId
      }
    });

    modal.afterClose.subscribe((result: IdentityRole) => {
      if (result) {
        treeNode.title = result.name;
        treeNode.data = result;
      }
    });
  }


  delete(treeNode: TreeNode<IdentityRole>, parent: TreeNode<IdentityRole>) {
    this.modalService.confirm({
      nzTitle: '确定删除吗?',
      nzOkText: '确定',
      nzCancelText: '取消',
      nzOnOk: () => {
        this.identityRoleService.delete(treeNode.key).subscribe(() => {
          this.messageService.success('删除成功');
          if (parent) {
            const treeNodeIndex = parent.children.findIndex(m => m.key === treeNode.key);
            parent.children.splice(treeNodeIndex, 1);
          } else {
            this.getList();
          }
        });
      }
    });
  }

  openPermissionsModal(treeNode: TreeNode<IdentityRole>) {
    console.log(treeNode);
    this.providerKey = treeNode.data.name;
    setTimeout(() => {
      this.visiblePermissions = true;
    }, 0);
  }

  expandChange(collapsed: boolean, item: TreeNode<IdentityRole>) {
    item.expand = collapsed;
  }
}
