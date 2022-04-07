﻿using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;

namespace Dignite.Abp.Identity
{
    public static class IdentityRoleListExtensions
    {
        public static IReadOnlyList<IdentityRoleDto> BuildIdentityRolesTree([NotNull] this IReadOnlyList<Volo.Abp.Identity.IdentityRoleDto> source)
        {
            var list = new List<IdentityRoleDto>();
            foreach (var item in source)
            {
                list.Add(new IdentityRoleDto(item));
            }

            return BuildIdentityRolesTree(list).AsReadOnly();
        }

        public static List<IdentityRoleDto> BuildIdentityRolesTree([NotNull] this IReadOnlyList<IdentityRoleDto> source)
        {
            //构建机构树
            var tree = new List<IdentityRoleDto>();
            tree.AddRange(source.Where(r => !r.ParentId.HasValue || !source.Any(s=> s.Id==r.ParentId)).ToList());
            foreach (var role in tree)
            {
                AddChildren(role, source);
            }

            return tree.ToList();
        }


        static void AddChildren(IdentityRoleDto parent, IReadOnlyList<IdentityRoleDto> list)
        {
            var children = list.Where(p => p.ParentId == parent.Id).ToList();
            if (children.Any())
            {
                parent.Children = children;

                foreach (var ou in children)
                {
                    AddChildren(ou, list);
                }
            }
        }
    }
}
