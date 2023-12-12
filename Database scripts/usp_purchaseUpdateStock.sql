USE [AnwiamServices]
GO

/****** Object:  StoredProcedure [dbo].[usp_purchaseUpdateStock]    Script Date: 12/12/2023 12:06:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE procedure [dbo].[usp_purchaseUpdateStock](@PName varchar(100),@Telephone varchar(15),@SpectacleRx varchar(100),@FrameType varchar(15),@FramePrice decimal,
@date DateTime,@LensType varchar(50),
@LensPrice decimal, @TotalPrice decimal, @AmountPaid decimal, @Quantity int)
As
begin
begin try
if @Quantity > (select Quantity from FrameStock where FrameTypeByColor=@FrameType)
begin
return -99;
end
begin tran

Declare @PatientId int;
if exists (select * from RefractionPx where Name=@PName and Telephone=@Telephone)
begin
select @PatientId=Id from RefractionPx where Name=@PName and Telephone=@Telephone
insert into Purchase values(@PatientId, @PName,@FrameType, @FramePrice,@LensType,@LensPrice,@TotalPrice,@AmountPaid,@date,@Quantity);
update FrameStock set Quantity=Quantity-@Quantity where FrameTypeByColor=@FrameType;
commit
return 1
end

if not exists(select * from RefractionPx where Name=@PName and Telephone=@Telephone)
begin
insert into RefractionPx values (@PName,@Telephone,@SpectacleRx);
set @PatientId=IDENT_CURRENT('RefractionPx');
insert into Purchase values(@PatientId,@PName, @FrameType, @FramePrice,@LensType,@LensPrice,@TotalPrice,@AmountPaid,@date,@Quantity);
update FrameStock set Quantity=Quantity-@Quantity where FrameTypeByColor=@FrameType;
commit
return 1
end
end try

begin catch
rollback
return 0
end catch
end
GO

