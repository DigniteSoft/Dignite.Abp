import { Injectable } from '@angular/core';
import { TreeNode, IdentityRole } from './organization-tree';

@Injectable({ providedIn: 'root' })
export class OrganizationTreeService<T extends IdentityRole> {

    /**
     * 根据list对象获取一整颗树，不支持懒加载
     * @param list 参数，包含一整棵数树结构
     *  @param expand 树默认是否展开
     */
    getTree(list: T[], expand = false): Array<TreeNode<T>> {

        const treeNodesContainer: TreeNode<T> = {} as TreeNode<T>;
        const treeNodes = this.getTreeNodes(list, 0, expand);
        const tree: TreeNode<T>[] = [];

        for (const treeNode of treeNodes) {

            if (!treeNode.key) {
                continue;
            }

            if (treeNodesContainer[treeNode.key]) {
                treeNode.children = treeNodesContainer[treeNode.key];
            } else {
                treeNode.children = treeNodesContainer[treeNode.key] = [];
            }

            if (!treeNode.parentId || !list.some(m => m.id === treeNode.parentId)) {
                tree.push(treeNode);
            } else {
                const parentId = treeNode.parentId;
                if (!treeNodesContainer[parentId]) {
                    treeNodesContainer[parentId] = [];
                }
                treeNodesContainer[parentId].push(treeNode);
            }
        }

        return tree.map(m => this.fullTreeStructureLevel(m));
    }

    /**
     * 将组织机构模型批量转换为树结点,children = []
     * @param list 需要转换的多个组织机构模型
     * @param level 当前模型所处层级
     * @param expand 转换后是否展开
     */
    getTreeNodes(list: T[], level = 0, expand = false): Array<TreeNode<T>> {
        return list.map<TreeNode<T>>(m => this.getTreeNode(m, level, expand));
    }

    /**
     * 将组织机构模型转换为树结点
     * @param entity 需要转换的组织机构模型
     * @param level 当前模型所处层级
     * @param expand 转换后是否展开
     */
    getTreeNode(entity: T, level = 0, expand = false): TreeNode<T> {
        return {
            key: entity.id,
            title: entity.name,
            parentId: entity.parentId,
            expand, level,
            data: entity,
            children: []
        } as TreeNode<T>;
    }

    /**
     * 私有方法，处理getTree()结果的层级结构
     */
    private fullTreeStructureLevel = (treeNode: TreeNode<T>, level = 0) => {
        treeNode.level = level++;

        if (treeNode.children.length) {
            // tslint:disable-next-line:variable-name
            for (const _treeNode of treeNode.children) {
                this.fullTreeStructureLevel(_treeNode, level);
            }
        }

        return treeNode;
    }
}

