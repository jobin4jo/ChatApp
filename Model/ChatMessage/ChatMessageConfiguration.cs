
using ChatApp.Model.ChatMessage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ChatMessageConfiguration : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.ToTable("tb_ChatMessage");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.SenderName).IsRequired();
        builder.Property(p => p.ReceiverName).IsRequired();
    }
}