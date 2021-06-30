
/* TableNameVariable */

set @tableNameQuoted = concat('`', @tablePrefix, 'ShippingPolicy`');
set @tableNameNonQuoted = concat(@tablePrefix, 'ShippingPolicy');


/* DropTable */

set @dropTable = concat('drop table if exists ', @tableNameQuoted);
prepare script from @dropTable;
execute script;
deallocate prepare script;
