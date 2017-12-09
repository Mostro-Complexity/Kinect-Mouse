function self_filter(path)
%% smooth函数参数
%'moving'就是所谓的平滑滤波。简单的平均而已
%'lowess'一阶多项式加权线性回归
%'loess'一阶多项式加权线性回归
%'rlowess'同'lowess'，但是自适应调整权值
%'rloess'同'loess'，但是自适应调整权值
%% smooth函数
src=load(path);
src=src(:,9:10);

figure
plot(src(:,1),src(:,2));
hold on
plot(smooth(src(:,1),'moving'),smooth(src(:,2),'moving'),'-');
hold on
plot(smooth(src(:,1),'lowess'),smooth(src(:,2),'lowess'),'-');
hold on
plot(smooth(src(:,1),'loess'),smooth(src(:,2),'loess'),'-');
hold on
plot(smooth(src(:,1),'rlowess'),smooth(src(:,2),'rlowess'));
hold on
plot(smooth(src(:,1),'rloess'),smooth(src(:,2),'rloess'));
hold off
legend('源数据','smooth.moving','smooth.lowess','smooth.loess',...
    'smooth.rlowess','smooth.rloess');

%% medfilt中值滤波
figure
plot(src(:,1),src(:,2));
hold on
plot(medfilt1(src(:,1),30),medfilt1(src(:,2),30))
hold off
legend('源数据','medfilt1','medfilt2');
end