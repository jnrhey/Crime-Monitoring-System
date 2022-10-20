using Android.Graphics;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Crime_Monitoring_Sys.GetSet;
using System;
using System.Collections.Generic;

namespace Crime_Monitoring_Sys.Adapters
{
    internal class com_admin_record_adapter : RecyclerView.Adapter
    {
        public event EventHandler<com_admin_record_adapterClickEventArgs> ItemClick;
        public event EventHandler<com_admin_record_adapterClickEventArgs> ItemLongClick;
        public event EventHandler<com_admin_record_adapterClickEventArgs> updateClick;
        List<comp_records_admin> items;

        public com_admin_record_adapter(List<comp_records_admin> data)
        {
            items = data;
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {

            View itemView = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.list_complaint_admin_layout, parent, false);


            var vh = new com_admin_record_adapterViewHolder(itemView, OnClick, OnLongClick, OnUpdateClick);
            return vh;
        }

    
        public override void OnBindViewHolder(RecyclerView.ViewHolder viewHolder, int position)
        {
            var holder = viewHolder as com_admin_record_adapterViewHolder;
            holder.name.Text = items[position].compName;
            holder.type.Text = items[position].compType;
            holder.description.Text = items[position].compDesc;
            holder.sender.Text = items[position].submittedBy;
            holder.time_stamp.Text = items[position].time_stamp;
            if (items[position].compStats == "Active")
            {
                holder.status.Text = "Active";
                holder.status.SetTextColor(Color.ParseColor("#FFBF00"));
            }
            else if (items[position].compStats != "Active")
            {
                holder.status.Text = "Solved";
                holder.status.SetTextColor(Color.ParseColor("#51A944"));
            }

        }

        public override int ItemCount => items.Count;

        void OnClick(com_admin_record_adapterClickEventArgs args) => ItemClick?.Invoke(this, args);
        void OnLongClick(com_admin_record_adapterClickEventArgs args) => ItemLongClick?.Invoke(this, args);
        void OnUpdateClick(com_admin_record_adapterClickEventArgs args) => updateClick?.Invoke(this, args);

    }

    public class com_admin_record_adapterViewHolder : RecyclerView.ViewHolder
    {
        //public TextView TextView { get; set; }
        public TextView name { get; set; }
        public TextView description { get; set; }
        public TextView type { get; set; }
        public TextView sender { get; set; }
        public TextView time_stamp { get; set; }
        public TextView status { get; set; }
        public Button update { get; set; }

        public com_admin_record_adapterViewHolder(View itemView, Action<com_admin_record_adapterClickEventArgs> clickListener,
                            Action<com_admin_record_adapterClickEventArgs> longClickListener, Action<com_admin_record_adapterClickEventArgs> updateClickListener) : base(itemView)
        {
            name = (TextView)itemView.FindViewById(Resource.Id.txtCompAdminUserName);
            type = (TextView)itemView.FindViewById(Resource.Id.txtComAdminType);
            description = (TextView)itemView.FindViewById(Resource.Id.txtComDescAdmin);
            sender = (TextView)itemView.FindViewById(Resource.Id.userSenderAdmin);
            time_stamp = (TextView)itemView.FindViewById(Resource.Id.txtCompAdminTime);
            status = (TextView)itemView.FindViewById(Resource.Id.txtCompAdminStatus);
            update = (Button)itemView.FindViewById(Resource.Id.btnUpdateCompStat);

            itemView.Click += (sender, e) => clickListener(new com_admin_record_adapterClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new com_admin_record_adapterClickEventArgs { View = itemView, Position = AdapterPosition });
            update.Click += (sender, e) => updateClickListener(new com_admin_record_adapterClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

    public class com_admin_record_adapterClickEventArgs : EventArgs
    {
        public View View { get; set; }
        public int Position { get; set; }
    }
}