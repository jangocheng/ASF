﻿using ASF.Domain.Values;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASF.Domain.Entities
{
    /// <summary>
    /// 管理员账户
    /// </summary>
    public class Account : IEntity
    {
        /// <summary>
        /// 管理员账户
        /// </summary>
        /// <param name="username">账户</param>
        /// <param name="password">密码</param>
        /// <param name="account">创建此账户管理员</param>
        public Account(string username, string password, Account account)
        {
            this.Username = username;
            this.Password = password;
            this.Name = username;
            this.Status = AccountStatus.Normal;
            this.LoginInfo = new LoginInfo();
            this.CreateInfo = new CreateOfAccount(account);
        }
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int Id { get; private set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Required, MaxLength(20)]
        public string Name { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required, StringLength(32, MinimumLength = 2)]
        public string Username { get; private set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        [Required, StringLength(20, MinimumLength = 6)]
        public string Password { get; private set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public PhoneNumber Telephone { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(225)]
        public string Avatar { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public AccountStatus Status { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; private set; }
        /// <summary>
        /// 角色集
        /// </summary>
        [Required]
        public IReadOnlyList<int> Roles { get; private set; } = new List<int>();
        /// <summary>
        /// 创建时信息
        /// </summary>
        [Required]
        public CreateOfAccount CreateInfo { get; private set; }
        /// <summary>
        /// 登录信息
        /// </summary>
        public LoginInfo LoginInfo { get; private set; }

        /// <summary>
        /// 设置登录密码
        /// </summary>
        /// <param name="password">登录密码</param>
        /// <returns></returns>
        public Result SetPassword(string password)
        {
            //为了安全新密码和旧密码不能一样
            if (this.Password == password)
                return Result.ReFailure(ResultCodes.AccountOldPasswordAndNewPasswordSame);

            this.Password = password;
            return Result.ReSuccess();
        }
        /// <summary>
        /// 是否和当前密码一致
        /// </summary>
        /// <param name="password">验证密码</param>
        /// <returns></returns>
        public bool HasPassword(string password)
        {
            return this.Password == password;
        }

        /// <summary>
        /// 设置分配的角色
        /// </summary>
        /// <param name="roles">角色编号</param>
        public void SetRoles(List<int> roles)
        {
            if (roles == null)
                roles = new List<int>();
            this.Roles = roles;
        }
        /// <summary>
        /// 是否允许登录
        /// </summary>
        /// <returns></returns>
        public bool IsAllowLogin()
        {
            if (!this.IsNormal())
                return false;
            if (this.Status != AccountStatus.Normal)
                return false;
            else
                return true;
        }
        /// <summary>
        /// 账户是否正常
        /// </summary>
        /// <returns></returns>
        public bool IsNormal()
        {
            if (this.IsDeleted)
                return false;
            else
                return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Delete()
        {
            this.IsDeleted = true;
        }


        /// <summary>
        /// 设置登录信息
        /// </summary>
        /// <param name="loginInfo"></param>
        public void SetLoginInfo(LoginInfo loginInfo)
        {
            this.LoginInfo = loginInfo;
        }
    }
}