﻿using AutoMapper.QueryableExtensions;
using PhotoShare.Data;
using PhotoShare.Models;
using PhotoShare.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoShare.Services
{
    public class UserService : IUserService
    {
        private readonly PhotoShareContext ctx;

        public UserService(PhotoShareContext ctx)
        {
            this.ctx = ctx;
        }

        public Friendship AcceptFriend(int userId, int friendId)
        {
            var friendship = new Friendship
            {
                UserId = userId,
                FriendId = friendId
            };

            this.ctx.Friendships.Add(friendship);

            this.ctx.SaveChanges();

            return friendship;
        }

        public Friendship AddFriend(int userId, int friendId)
        {
            var friendship = new Friendship
            {
                UserId = userId,
                FriendId = friendId
            };

            this.ctx.Friendships.Add(friendship);

            this.ctx.SaveChanges();

            return friendship;
        }

        public TModel ById<TModel>(int id) => this.By<TModel>(x => x.Id == id).SingleOrDefault();

        public TModel ByUsername<TModel>(string username) => this.By<TModel>(x => x.Username == username).SingleOrDefault();

        public void ChangePassword(int userId, string password)
        {
            var user = this.ctx.Users.Find(userId);

            user.Password = password;

            this.ctx.SaveChanges();
        }

        public void Delete(string username)
        {
            var user = this.ByUsername<User>(username);

            user.IsDeleted = true;

            this.ctx.SaveChanges();
        }

        public bool Exists(int id) => this.ById<User>(id) != null;

        public bool Exists(string name) => this.ByUsername<User>(name) != null;

        public User Register(string username, string password, string email)
        {
            var user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false
            };

            this.ctx.Users.Add(user);

            this.ctx.SaveChanges();

            return user;
        }

        public void SetBornTown(int userId, int townId)
        {
            var user = this.ctx.Users.Find(userId);

            user.BornTownId = townId;

            this.ctx.SaveChanges();
        }

        public void SetCurrentTown(int userId, int townId)
        {
            var user = this.ctx.Users.Find(userId);

            user.CurrentTownId = townId;

            this.ctx.SaveChanges();
        }

        private IEnumerable<TModel> By<TModel>(Func<User, bool> predicate) => this.ctx.Users.Where(predicate).AsQueryable().ProjectTo<TModel>();
    }
}