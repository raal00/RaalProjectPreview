var customerPanelVue = new Vue({
	el: '#customerPanelVue',
	data:
	{
		Orders: [],
		Items: [],
		CaseItems: [],
		OnlyNewOrders: false
	},
	methods:
	{
		GetItemList: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Customer/GetItemList",
				success: function (response) {
					vue.Items = response.Items;
					if (vue.Items.length == 0) alert('Ничего не найдено');
				}
			});
		},
		AddItemToCase: function (id) {
			var vue = this;
			var indx = vue.Items[id].Id;
			dataPost = {
				ItemId: indx
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: "/Customer/AddItemToCase",
				success: function (response) {
					vue.GetMyCase();
				}
			});
		},
		CreateOrder: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Customer/CreateOrder",
				success: function (response) {
					vue.ShowMyOrders();
					vue.GetMyCase();
				}
			});
		},
		ShowMyOrders: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Customer/ShowMyOrders",
				success: function (response) {
					vue.Orders = response.Orders;
					if (response.Orders.length == 0) alert('Ничего не найдено');
				}
			});
		},
		CloseOrder: function (id) {
			var vue = this;
			var indx = vue.Orders[id].Id;
			dataPost = {
				OrderId: indx
			};
			$.ajax({
				data: dataPost,
				type: 'POST',
				url: "/Customer/CloseOrder",
				success: function (response) {
					vue.ShowMyOrders();
					vue.GetMyCase();
				}
			});
		},
		GetMyCase: function () {
			var vue = this;
			$.ajax({
				type: 'POST',
				url: "/Customer/MyCase",
				success: function (response) {
					vue.CaseItems = response.CustomerCase;
				}
			});
        }
	},
	computed:
	{
		filteredOrderList() {
			var vue = this;
			return vue.Orders.filter(order => {
				if (order.Status == 'New' || vue.OnlyNewOrders == false) return order;
			})
		}
	}
});