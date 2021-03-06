function gen_gif(srcpath,name,tarpath)
%% 批量产生图片
star=load(srcpath);
star=star(:,9:10);
handler=figure('Visible','off');
for i=1:10:size(star,1)
    plot(star(1:i,1),star(1:i,2),'r');
    hold on
    plot(kalman_filter(star(1:i,1),1e-5,4e-4,star(1,1),1),...
        kalman_filter(star(1:i,2),1e-5,4e-4,star(2,2),1),'b');
    legend('原数据','卡尔曼滤波后数据');
    filename=[tarpath name num2str(i)];
    saveas(handler,filename,'jpg');
end  
%% 产生gif
for j=1:10:i  
    im=imread([tarpath name num2str(j) '.jpg']);  
    [I,map]=rgb2ind(im,20); 
    filename=[tarpath name '.gif'];
    if j==1  
       imwrite(I,map,filename,'gif', 'Loopcount',inf,'DelayTime',0.2);%FIRST  
    else  
       imwrite(I,map,filename,'gif','WriteMode','append','DelayTime',0.2);  
    end  
    close all
end  
end
% gen_gif('cross.txt','cross','./gif/cross/')
% gen_gif('star.txt','star','./gif/star/')