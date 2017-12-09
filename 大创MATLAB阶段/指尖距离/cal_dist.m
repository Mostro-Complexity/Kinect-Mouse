%% 计算两个点的距离-函数
function len=cal_dist(a,b)
    len=zeros(size(a,1),1);
    for i=1:size(a,2)
    len=len+(a(:,i)-b(:,i)).^2;
    end
len=sqrt(len);            
end