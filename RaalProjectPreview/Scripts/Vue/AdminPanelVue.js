var adminPanelVue = new Vue({
	el: '#adminPanelVue',
	data:
	{
		Users: [],
		Orders: [],
		Items: [],
		NewItem: {},
		NewUser: {},
		SelectedUser: {},
		SelectedItem: {}
	},
	methods:
	{
		ShowAllUsers: function () {
			var vue = this;
			$.ajax({
				type: 'GET',
				url: "/Admin/ShowUsers",
				success: function (response) {
					vue.Users = response.UserList;
				}
			});
        },
		ShowAllOrders: function() {
			var vue = this;
			$.ajax({
				type: 'GET',
				url: "/Admin/ShowOrders",
				success: function (response) {
					vue.Orders = response.Orders;
				}
			});
		},
		ShowAllItems: function () {
			var vue = this;
			$.ajax({
				type: 'GET',
				url: "/Admin/ShowItems",
				success: function (response) {
					vue.Items = response.Items;
				}
			});
		},
		AddNewItem: function () {
			var vue = this;
			dataPost = {
				NewItem: vue.NewItem
			};
			$.ajax({
				type: 'POST',
				url: "/Admin/AddItemToShop",
				success: function (response) {
					console.log(response);
				}
			});
		},
		AddNewUser: function () {
			var vue = this;
			dataPost = {
				AllUserData: vue.NewUser
			};
			$.ajax({
				type: 'POST',
				url: "/Admin/AddNewUser",
				success: function (response) {
					console.log(response);
				}
			});
		},

		EditUser: function () {
			var vue = this;
			dataPost = {
				AllUserData: vue.SelectedUser
			};
			$.ajax({
				type: 'POST',
				url: "/Admin/EditUser",
				success: function (response) {
					console.log(response);
				}
			});
		},
		EditItem: function () {
			var vue = this;
			dataPost = {
				Item: vue.SelectedItem
			};
			$.ajax({
				type: 'POST',
				url: "/Admin/EditItemInShop",
				success: function (response) {
					console.log(response);
				}
			});
		},
		DeleteUser: function (id) {
			var vue = this;
			dataPost = {
				UserId: id
			};
			$.ajax({
				type: 'POST',
				url: "/Admin/DeleteUser",
				success: function (response) {
					console.log(response);
				}
			});
		},
		DeleteItem: function (id) {
			var vue = this;
			dataPost = {
				ItemId: id
			};
			$.ajax({
				type: 'POST',
				url: "/Admin/DeleteItemFromShop",
				success: function (response) {
					console.log(response);
				}
			});
		},
		SetCompletedOrderStatus: function (id) {
			var vue = this;
			dataPost = {
				OrderId = id
			};
			$.ajax({
				type: 'POST',
				url: "/Admin/SetCompletedOrderStatus",
				success: function (response) {
					console.log(response);
				}
			});
        },
		SetInProcessingOrderStatus: function (id) {
			var vue = this;
			dataPost = {
				OrderId = id
			};
			$.ajax({
				type: 'POST',
				url: "/Admin/SetInProcessingOrderStatus",
				success: function (response) {
					console.log(response);
				}
			});
		}

	}
});