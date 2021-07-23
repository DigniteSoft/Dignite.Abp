import { IdentityRoleDto } from '@abp/ng.identity';

export type IdentityRole = {parentId?: string} & IdentityRoleDto;

export interface TreeNode<T> {
    key: string;
    title: string;
    parentId: string;
    data: T;
    level: number;
    expand: boolean;
    children: Array<TreeNode<T>>;
}
